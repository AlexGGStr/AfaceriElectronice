import React, { FC } from "react";
import styles from "./CheckoutPanel.module.css";
import CartList from "../CartList/CartList";
import LinkItem from "../../UI/Link/Link";
import ListItem from "../../UI/ListItem/ListItem";

interface CheckoutPanelProps {}

const CheckoutPanel: FC<CheckoutPanelProps> = () => (
  <>
    <CartList checkout={true} />
    <ListItem>
      <LinkItem to={"/checkout/payment"}>Choose Payment Method</LinkItem>
    </ListItem>
  </>
);

export default CheckoutPanel;
