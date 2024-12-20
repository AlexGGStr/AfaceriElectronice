import React, { FC, useContext } from "react";
import styles from "./AddCartButton.module.css";
import Button from "../../UI/Button/Button";
import { useRouteLoaderData } from "react-router-dom";
import useApiCall from "../../util/useApiCall";
import Product from "../../models/Product";
import CartItem from "../../models/CartItem";
import backendUrl from "../../config";
import Response from "../../models/Response";
import CartContext from "../../util/cart-context";

interface AddCartButtonProps {
  product?: Product | null;
  className?: string;
}

function addToLocalStorage(product: Product) {
  const cart: CartItem[] = localStorage.getItem("cart")
    ? JSON.parse(localStorage.getItem("cart")!)
    : [];

  const existingCartItem = cart.find((item) => item.product.id === product!.id);

  if (existingCartItem) {
    existingCartItem.quantity += 1;
  } else {
    cart.push({ product: product, quantity: 1 });
  }

  localStorage.setItem("cart", JSON.stringify(cart));
}

async function addToDb(token: any, product: Product | null | undefined) {
  const data = await fetch(`${backendUrl}/Cart`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify([{ productId: product!.id, quantity: 1 }]),
  });
  const response: Response<boolean> = await data.json();
  response.success ? alert("Added to cart") : alert("Failed to add to cart");
}

const AddCartButton: FC<AddCartButtonProps> = ({ product, className }) => {
  const token = useRouteLoaderData("root");
  const ctx = useContext(CartContext);

  const toCartHandler = async () => {
    console.log(token);
    if (token) {
      await addToDb(token, product);
    }
    product && ctx.onAddItem({ product: product, quantity: 1 });
  };

  return (
    <Button
      className={` mt-1 text-white ${className}`}
      onClick={toCartHandler}
      disabled={!(product && product?.quantity > 0)}
      title={!(product && product?.quantity > 0) ? "Out of stock" : undefined}
    >
      Add to cart
    </Button>
  );
};

export default AddCartButton;
