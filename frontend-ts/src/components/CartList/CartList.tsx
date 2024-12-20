import React, { FC, useCallback, useContext, useEffect, useState } from "react";
import styles from "./CartList.module.css";
import useApiCall from "../../util/useApiCall";
import Product from "../../models/Product";
import backendUrl from "../../config";
import { useRouteLoaderData, useSearchParams } from "react-router-dom";
import CartItem from "../../models/CartItem";
import Response from "../../models/Response";
import CartCard from "../CartCard/CartCard";
import CartContext from "../../util/cart-context";
import CheckoutCard from "../CheckoutCard/CheckoutCard";

interface CartListProps {
  checkout?: boolean;
}

function useFetchCartData(token: any): [CartItem[], boolean, () => void] {
  const ctx = useContext(CartContext);

  const [data, setData] = useState<CartItem[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const refetchData = useCallback(async () => {
    try {
      const response = await fetch(`${backendUrl}/Cart`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          Authorization: `Bearer ${token}`,
        },
      });
      const jsonData: Response<CartItem[]> = await response.json();
      setData(jsonData.data);
      // ctx.onSetItems(jsonData.data);
      setIsLoading(false);
    } catch (error) {
      console.error(error);
    }
  }, [token]);

  useEffect(() => {
    if (token) {
      refetchData();
    } else {
      setData(ctx.items);
      setIsLoading(false);
    }
  }, [token, refetchData, ctx.items]);

  return [data, isLoading, refetchData];
}

const CartList: FC<CartListProps> = ({ checkout = false }) => {
  const ctx = useContext(CartContext);
  const token = useRouteLoaderData("root");
  const [data, isLoading, refetchData] = useFetchCartData(token);

  let content = <p>Cart is Empty</p>;

  if (data && data.length > 0) {
    content = (
      <ul className="flex w-full justify-center items-center flex-col ">
        {data.map((item) => (
          <li
            className="w-full mt-3 ml-1 p-1 items-center justify-center flex"
            key={item.product.id}
          >
            {!checkout ? (
              <CartCard
                key={item.product.id}
                item={item}
                onDelete={refetchData}
              />
            ) : (
              <CheckoutCard key={item.product.id} item={item} />
            )}
          </li>
        ))}
      </ul>
    );
  }

  if (isLoading) {
    content = <p>Loading...</p>;
  }

  return <div className="flex w-full items-center">{content}</div>;
};

export default CartList;
