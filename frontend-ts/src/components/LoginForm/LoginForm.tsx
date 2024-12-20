import React, { FC, useContext, useState } from "react";
import styles from "./LoginForm.module.css";
import Card from "../../UI/Card/Card";
import Button from "../../UI/Button/Button";
import Response from "../../models/Response";
import AuthField from "../AuthField/AuthField";
import { useNavigate } from "react-router-dom";
import backendUrl, { clientId } from "../../config";
import CartItem from "../../models/CartItem";
import CartContext from "../../util/cart-context";
import { CredentialResponse, GoogleLogin } from "@react-oauth/google";
import GoogleButton from "../GoogleButton/GoogleButton";
import { addCartToDb } from "../../util/addCartToDb";

interface LoginFormProps {}

const LoginForm: FC<LoginFormProps> = () => {
  const [enteredEmail, setEnteredEmail] = useState("");
  const [enteredPassword, setEnteredPassword] = useState("");
  const [response, setResponse] = useState<Response<string> | null>(null);
  const navigate = useNavigate();
  const ctx = useContext(CartContext);

  const submitHandler = async (event: any) => {
    event.preventDefault();
    const data = await fetch(`${backendUrl}/Auth/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: enteredEmail,
        password: enteredPassword,
      }),
    });

    const dataJson: Response<string> = await data.json();
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
    <Card className={styles.login}>
      <form onSubmit={submitHandler}>
        <AuthField
          value={enteredEmail}
          onChange={(event) => setEnteredEmail(event.target.value)}
        >
          E-mail
        </AuthField>
        <AuthField
          value={enteredPassword}
          onChange={(event) => setEnteredPassword(event.target.value)}
          type="password"
        >
          Password
        </AuthField>
        {response && !response.success ? (
          <div className="flex justify-center text-red-800">
            {response.message}
          </div>
        ) : null}
        <div className={styles.actions}>
          <Button className="mt-10" type="submit">
            Login
          </Button>
        </div>
      </form>
      <GoogleButton />
    </Card>
  );
};

export default LoginForm;
