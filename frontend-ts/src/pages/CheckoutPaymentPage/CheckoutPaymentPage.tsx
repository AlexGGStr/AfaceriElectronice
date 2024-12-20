import React, { FC, useContext, useEffect } from "react";
import styles from "./CheckoutPaymentPage.module.css";
import PaymentList from "../../components/PaymentList/PaymentList";
import ListItem from "../../UI/ListItem/ListItem";
import LinkItem from "../../UI/Link/Link";
import AdressList from "../../components/AdressList/AdressList";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import Button from "../../UI/Button/Button";
import useApiCall from "../../util/useApiCall";
import UserAdress from "../../models/UserAdress";
import backendUrl from "../../config";
import UserPayment from "../../models/UserPayment";
import CartContext from "../../util/cart-context";
import Response from "../../models/Response";
import internal from "stream";

interface CheckoutPaymentPageProps {}

const CheckoutPaymentPage: FC<CheckoutPaymentPageProps> = () => {
  const [params] = useSearchParams();
  const paymentId = params.get("paymentId");
  const adressId = params.get("adressId");
  const ctx = useContext(CartContext);
  const navigate = useNavigate();

  const { data: adressData, isLoading: adressLoading } = useApiCall<UserAdress>(
    `${backendUrl}/UserAdress/${adressId}`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    }
  );

  const { data: paymentData, isLoading: paymentLoading } =
    useApiCall<UserPayment>(`${backendUrl}/UserPayment/${paymentId}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    });

  const valuesSelected = async (
    e: React.MouseEvent<HTMLAnchorElement, MouseEvent>
  ) => {
    if (
      paymentId == "null" ||
      adressId == "null" ||
      paymentId == null ||
      adressId == null ||
      adressLoading ||
      paymentLoading
    ) {
      alert("Please select a payment method and an adress");
      return e.preventDefault();
    }

    const res = await fetch(`${backendUrl}/PlaceOrder`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      body: JSON.stringify({
        adress: {
          adressLine: adressData?.data.adressLine,
          city: adressData?.data.city,
          postalCode: adressData?.data.postalCode,
          telephone: adressData?.data.telephone,
        },
        payment: {
          paymentType: paymentData?.data.paymentType,
          accountNo: paymentData?.data.accountNo,
        },
        total: ctx.getPrice(),
      }),
    });
    const response: Response<number> = await res.json();
    if (response.success) {
      alert("Order placed");
      ctx.onSetItems([]);
      navigate("/account/orders");
    }
  };

  return (
    <div className="flex grid grid-cols-2">
      <PaymentList checkout={true} />
      <AdressList checkout={true} />
      <Button
        onClick={valuesSelected}
        className="flex p-2 rounded-md w-full justify-center items-center"
      >
        Place Order
      </Button>
    </div>
  );
};

export default CheckoutPaymentPage;
