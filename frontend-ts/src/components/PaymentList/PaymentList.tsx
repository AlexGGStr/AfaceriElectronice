import React, { FC } from "react";
import styles from "./PaymentList.module.css";
import Card from "../../UI/Card/Card";
import ListItem from "../../UI/ListItem/ListItem";
import LinkItem from "../../UI/Link/Link";
import UserPayment from "../../models/UserPayment";
import backendUrl from "../../config";
import useApiCall from "../../util/useApiCall";
import { useSearchParams } from "react-router-dom";

interface PaymentListProps {
  checkout?: boolean;
}

const PaymentList: FC<PaymentListProps> = ({ checkout = false }) => {
  const [params] = useSearchParams();
  const paymentId = params.get("paymentId");
  const adressId = params.get("adressId");

  const { data, isLoading } = useApiCall<UserPayment[]>(
    `${backendUrl}/UserPayment`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    }
  );

  let content = <p>No payments found</p>;

  if (!isLoading && data && data.data.length > 0) {
    content = (
      <>
        {data.data.map((payment) => (
          <ListItem
            key={payment.id}
            className={`${
              payment.id.toString() === paymentId ? "bg-green-500" : ""
            }`}
          >
            <LinkItem
              to={
                !checkout
                  ? `/account/payments/${payment.id}`
                  : `?adressId=${adressId}&paymentId=${payment.id}`
              }
            >
              {payment.accountNo}
            </LinkItem>
          </ListItem>
        ))}
      </>
    );
  }

  if (isLoading) {
    content = <p>Loading...</p>;
  }

  return (
    <div
      className={`flex ${!checkout ? `justify-center` : ""} items-center mt-1`}
    >
      <Card className={`${!checkout ? "w-1/2" : "w-full m-3"} justify-center`}>
        <div className="flex justify-center">
          <Card className="w-full bg-gray-100 rounded-2xl m-4 items-center text-center justify-center">
            <ul className="list-none w-full">{content}</ul>
          </Card>
        </div>
        <ListItem className="mx-4 list-none">
          <LinkItem to={"/account/payments/new"}>Add Payment Method</LinkItem>
        </ListItem>
      </Card>
    </div>
  );
};

export default PaymentList;
