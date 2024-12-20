import React, { FC } from "react";
import styles from "./NewPaymentPage.module.css";
import NewPaymentForm from "../../components/NewPaymentForm/NewPaymentForm";
import backendUrl from "../../config";

interface NewPaymentPageProps {}

const NewPaymentPage: FC<NewPaymentPageProps> = () => {
  const submitFormHandler = async (
    event: React.FormEvent<HTMLFormElement>,
    accountNo: string,
    paymentType: string
  ) => {
    event.preventDefault();
    await fetch(`${backendUrl}/UserPayment`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      body: JSON.stringify({
        accountNo: accountNo,
        paymentType: paymentType,
      }),
    });
    return window.location.replace("/account/payments");
  };

  return <NewPaymentForm onSubmit={submitFormHandler} />;
};

export default NewPaymentPage;
