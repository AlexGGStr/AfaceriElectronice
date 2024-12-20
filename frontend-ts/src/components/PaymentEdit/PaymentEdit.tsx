import React, { FC, useEffect, useState } from "react";
import styles from "./PaymentEdit.module.css";
import { useParams } from "react-router-dom";
import UserPayment from "../../models/UserPayment";
import useApiCall from "../../util/useApiCall";
import backendUrl from "../../config";
import Card from "../../UI/Card/Card";
import AuthField from "../AuthField/AuthField";
import Button from "../../UI/Button/Button";

interface PaymentEditProps {}

const PaymentEdit: FC<PaymentEditProps> = () => {
  const params = useParams();

  const { data, isLoading } = useApiCall<UserPayment>(
    `${backendUrl}/UserPayment/${params.paymentId}`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    }
  );

  const [paymentType, setPaymentType] = useState<string>("Loading...");
  const [accountNo, setAccountNo] = useState<string>("Loading...");

  const changePaymentTypeHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPaymentType(event.target.value);
  };

  const changeAccountNoHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setAccountNo(event.target.value);
  };

  useEffect(() => {
    if (!isLoading && data) {
      setPaymentType(data.data.paymentType ?? "");
      setAccountNo(data.data.accountNo ?? "");
    }
  }, [data, isLoading]);

  const submitFormHandler = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    await fetch(`${backendUrl}/UserPayment/${params.paymentId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      body: JSON.stringify({
        paymentType: paymentType,
        accountNo: accountNo,
      }),
    });
    alert("Payment edited successfully!");
    return window.location.replace("/account/payments");
  };

  const deleteHandler = async () => {
    const check = window.confirm(
      "Are you sure you want to delete this payment?"
    );
    if (check) {
      await fetch(`${backendUrl}/UserPayment/${params.paymentId}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });
      return window.location.replace("/account/payments");
    }
  };

  return (
    <Card className="w-90 max-w-3xl mx-auto my-8 p-8">
      <form onSubmit={submitFormHandler}>
        <AuthField
          required={true}
          value={paymentType}
          onChange={changePaymentTypeHandler}
        >
          Payment type
        </AuthField>
        <AuthField
          required={true}
          value={accountNo}
          onChange={changeAccountNoHandler}
        >
          Account number
        </AuthField>
        <div>
          <Button type="submit">Edit Payment</Button>
        </div>
      </form>
      <Button className="mt-1" onClick={deleteHandler}>
        Delete Payment
      </Button>
    </Card>
  );
};

export default PaymentEdit;
