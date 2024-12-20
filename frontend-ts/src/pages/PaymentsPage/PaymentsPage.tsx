import React, { FC } from "react";
import styles from "./PaymentsPage.module.css";
import PaymentList from "../../components/PaymentList/PaymentList";

interface PaymentsPageProps {}

const PaymentsPage: FC<PaymentsPageProps> = () => <PaymentList />;

export default PaymentsPage;
