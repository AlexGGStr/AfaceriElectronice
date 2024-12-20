import CartItem from "../models/CartItem";
import { createContext, useEffect, useState } from "react";
import { useRouteLoaderData } from "react-router-dom";
import backendUrl from "../config";

const CartContext = createContext<{
  items: CartItem[];
  onAddItem: (item: CartItem) => void;
  onRemoveItem: (item: CartItem, token?: string | undefined) => void;
  onClearCart: () => void;
  onSetItems: (data: CartItem[]) => void;
  getItem: (id: number) => CartItem | undefined;
  getPrice: () => number;
}>({
  items: [],
  onAddItem: () => {},
  onRemoveItem: () => {},
  onClearCart: () => {},
  onSetItems: () => {},
  getItem: () => undefined,
  getPrice: () => 0,
});

export const CartContextProvider = (props: any) => {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);

  useEffect(() => {
    const storedItems = JSON.parse(localStorage.getItem("cart") || "[]");

    if (storedItems.length > 0) {
      setCartItems(storedItems);
    }
  }, []);

  const addItemToCart = (item: CartItem) => {
    const newCartItems = [...cartItems];
    const existingCartItem = newCartItems.find(
      (cartItem) => cartItem.product.id === item.product.id
    );
    if (existingCartItem) {
      existingCartItem.quantity += 1;
    } else {
      newCartItems.push(item);
    }
    setCartItems(newCartItems);
    localStorage.setItem("cart", JSON.stringify(newCartItems));
  };

  const removeItemFromCart = (
    item: CartItem,
    token: string | undefined = undefined
  ) => {
    const newCartItems = [...cartItems];
    const existingCartItem = newCartItems.find(
      (cartItem) => cartItem.product.id === item.product.id
    );

    if (existingCartItem) {
      existingCartItem.quantity -= 1;

      if (existingCartItem.quantity === 0) {
        const check = window.confirm(
          "Are you sure you want to remove this item from cart?"
        );

        if (check) {
          const newCartItems = cartItems.filter(
            (cartItem) => cartItem.product.id !== item.product.id
          );
          setCartItems(newCartItems);
          localStorage.setItem("cart", JSON.stringify(newCartItems));

          if (token) {
            fetch(`${backendUrl}/Cart/${item.product.id}`, {
              method: "DELETE",
              headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
              },
            });
          }
        }
      } else {
        setCartItems(newCartItems);
        localStorage.setItem("cart", JSON.stringify(newCartItems));
      }
    }
  };

  const clearCart = () => {
    setCartItems([]);
    localStorage.removeItem("cart");
  };

  const getItem = (id: number) => {
    return cartItems.find((item) => item.product.id === id);
  };

  const setItemsToCart = (data: CartItem[]) => {
    setCartItems(data);
    localStorage.setItem("cart", JSON.stringify(data));
  };

  const getTotalPrice = () => {
    return cartItems.reduce((acc, item) => {
      return acc + item.product.price * item.quantity;
    }, 0);
  };

  return (
    <CartContext.Provider
      value={{
        items: cartItems,
        onAddItem: addItemToCart,
        onRemoveItem: removeItemFromCart,
        onClearCart: clearCart,
        onSetItems: setItemsToCart,
        getItem: getItem,
        getPrice: getTotalPrice,
      }}
    >
      {props.children}
    </CartContext.Provider>
  );
};

export default CartContext;
