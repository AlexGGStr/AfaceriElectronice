import React, { FC, useContext, useEffect, useState } from "react";
import styles from "./CartCard.module.css";
import Product from "../../models/Product";
import { Card } from "react-bootstrap";
import { Link, useRouteLoaderData } from "react-router-dom";
import AddCartButton from "../AddCartButton/AddCartButton";
import CartItem from "../../models/CartItem";
import Button from "../../UI/Button/Button";
import backendUrl from "../../config";
import CartContext from "../../util/cart-context";
import product from "../../models/Product";
import Response from "../../models/Response";

interface CartCardProps {
  item: CartItem;
  onDelete: () => void;
}

const CartCard: FC<CartCardProps> = ({ item, onDelete }) => {
  const [quantity, setQuantity] = useState(item.quantity);
  const token: any = useRouteLoaderData("root");
  const ctx = useContext(CartContext);

  const minusClickHandler = async () => {
    setQuantity(() => {
      return quantity - 1;
    });
    if (token) {
      updateToDb(token, item.product.id, false);
    }

    onDelete();
    ctx.onRemoveItem(item, token);
  };

  async function updateToDb(token: any, productId: number, add: boolean) {
    const data = await fetch(`${backendUrl}/Cart/${productId}?add=${add}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });
    const response: Response<number> = await data.json();
    setQuantity(response.data);
  }

  const plusClickHandler = () => {
    setQuantity(() => {
      return quantity + 1;
    });
    if (token) {
      updateToDb(token, item.product.id, true);
    }
    ctx.onAddItem({ product: item.product, quantity: 1 });
  };

  return (
    <div className="flex w-3/5 rounded-2xl -mx-4 shadow items-center justify-between">
      <div className="flex flex-row items-center">
        <img
          className=" pl-10 w-36 h-52 object-contain "
          src={item.product.images[0]?.imageName || "/images/default.jpg"}
          alt="Image description"
        />
        <h3 className=" font-bold text-xl pl-14 text-center">
          {item.product.name}
        </h3>
      </div>
      <div className="flex flex-row items-center">
        <Button className="m-1 w-0.5" onClick={minusClickHandler}>
          -
        </Button>
        <div className="items-center">{quantity}</div>
        <Button className="m-1 w-0.5" onClick={plusClickHandler}>
          +
        </Button>
      </div>
    </div>
  );
};
export default CartCard;
