import React, { FC, useContext, useState } from "react";
import styles from "./GoogleButton.module.css";
import { CredentialResponse, GoogleLogin } from "@react-oauth/google";
import backendUrl from "../../config";
import Response from "../../models/Response";
import { useNavigate } from "react-router-dom";
import CartContext from "../../util/cart-context";
import { addCartToDb } from "../../util/addCartToDb";

interface GoogleButtonProps {}

const GoogleButton: FC<GoogleButtonProps> = () => {
  const [response, setResponse] = useState<Response<string> | null>(null);
  const navigate = useNavigate();
  const ctx = useContext(CartContext);

  const googleAuthHandler = async (res: CredentialResponse) => {
    const data = await fetch(`${backendUrl}/Auth/google`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        token: res.credential,
      }),
    });

    const dataJson: Response<string> = await data.json();
    console.log(dataJson);
    setResponse(dataJson);
    if (dataJson.success) {
      localStorage.setItem("token", dataJson.data);

      const cart = await addCartToDb(ctx.items);
      ctx.onSetItems(cart!);

      alert("Login successful");

      navigate("/");
    }
  };

  return (
    <div className="w-full">
      <GoogleLogin
        onSuccess={googleAuthHandler}
        onError={() => {
          console.log("Error on google auth");
        }}
      />
    </div>
  );
};
export default GoogleButton;
