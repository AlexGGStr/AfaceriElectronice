import React, { FC } from "react";
import styles from "./CartPage.module.css";
import CartList from "../../components/CartList/CartList";
import ListItem from "../../UI/ListItem/ListItem";
import LinkItem from "../../UI/Link/Link";

interface CartPageProps {}

const CartPage: FC<CartPageProps> = () => (
  <div className="flex flex-col  items-center">
    <CartList />
    <ListItem className="w-3/5 flex justify-center">
      <LinkItem to={"/checkout"}>Checkout</LinkItem>
    </ListItem>
  </div>
);

export default CartPage;
