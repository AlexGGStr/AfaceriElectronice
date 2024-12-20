import React, { FC } from "react";
import styles from "./CheckoutPage.module.css";
import CartList from "../../components/CartList/CartList";
import CheckoutPanel from "../../components/CheckoutPanel/CheckoutPanel";

interface CheckoutPageProps {}

const CheckoutPage: FC<CheckoutPageProps> = () => <CheckoutPanel />;

export default CheckoutPage;
