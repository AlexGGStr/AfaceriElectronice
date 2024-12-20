import React, { FC } from "react";
import styles from "./PaymentDetail.module.css";
import PaymentEdit from "../../components/PaymentEdit/PaymentEdit";

interface PaymentDetailProps {}

const PaymentDetail: FC<PaymentDetailProps> = () => <PaymentEdit />;

export default PaymentDetail;
