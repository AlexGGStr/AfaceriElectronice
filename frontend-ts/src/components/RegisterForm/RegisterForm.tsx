import React, { FC, useEffect, useState } from "react";
import styles from "./RegisterForm.module.css";
import AuthField from "../AuthField/AuthField";
import Card from "../../UI/Card/Card";
import Button from "../../UI/Button/Button";
import { Link, redirect, useNavigate } from "react-router-dom";
import Response from "../../models/Response";
import backendUrl from "../../config";
import response from "../../models/Response";
import GoogleButton from "../GoogleButton/GoogleButton";

interface RegisterFormProps {}

const RegisterForm: FC<RegisterFormProps> = () => {
  const [enteredFirstName, setEnteredFirstName] = useState("");
  const [enteredLastName, setEnteredLastName] = useState("");
  const [enteredUsername, setEnteredUsername] = useState("");
  const [usernameIsValid, setUsernameIsValid] = useState(true);
  const [enteredEmail, setEnteredEmail] = useState("");
  const [emailIsValid, setEmailIsValid] = useState(true);
  const [enteredPassword, setEnteredPassword] = useState("");
  const [passwordIsValid, setPasswordIsValid] = useState(true);
  const [confirmedPassword, setconfirmedPassword] = useState("");
  const [confirmedPasswordIsValid, setconfirmedPasswordIsValid] =
    useState(true);
  const [response, setResponse] = useState<Response<string> | null>(null);
  const navigate = useNavigate();

  const [formIsValid, setFormIsValid] = useState(false);

  useEffect(() => {
    const identifier = setTimeout(() => {
      setFormIsValid(
        enteredEmail.includes("@") &&
          enteredPassword.trim().length > 6 &&
          enteredUsername.trim().length > 5 &&
          confirmedPassword === enteredPassword
      );
    });

    return () => {
      clearTimeout(identifier);
    };
  }, [enteredEmail, enteredPassword, enteredUsername, confirmedPassword]);

  const emailChangeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    setEnteredEmail(event.target.value);
  };

  const firstNameChangeHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setEnteredFirstName(event.target.value);
  };

  const lastNameChangeHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setEnteredLastName(event.target.value);
  };

  const passwordChangeHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setEnteredPassword(event.target.value);
  };

  const validateEmailHandler = () => {
    setEmailIsValid(enteredEmail.includes("@"));
  };

  const validatePasswordHandler = () => {
    setPasswordIsValid(enteredPassword.trim().length > 6);
  };

  const usernameChangeHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setEnteredUsername(event.target.value);
  };

  const validateUsernameHandler = () => {
    setUsernameIsValid(enteredUsername.trim().length > 5);
  };

  const confirmedPasswordChangeHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setconfirmedPassword(event.target.value);
  };

  const validateConfirmedPasswordHandler = () => {
    setconfirmedPasswordIsValid(confirmedPassword === enteredPassword);
  };

  const submitFormHandler = async (event: any) => {
    event.preventDefault();
    const data = await fetch(`${backendUrl}/Auth/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        username: enteredUsername,
        password: enteredPassword,
        email: enteredEmail,
        firstname: enteredFirstName,
        lastname: enteredLastName,
      }),
    });
    const response: Response<string> = await data.json();
    setResponse(response);

    if (response && !response.success) {
      if (response.message.toLowerCase() === "username already exists") {
        setUsernameIsValid(false);
      }
      if (response.message.toLowerCase() === "email already exists") {
        setEmailIsValid(false);
      }
    } else {
      alert("User created successfully");
      navigate("/auth?login=true");
    }
  };
  return (
    <Card className={styles.login}>
      <form onSubmit={submitFormHandler}>
        <AuthField
          required={true}
          value={enteredFirstName}
          onChange={firstNameChangeHandler}
        >
          First Name
        </AuthField>
        <AuthField
          required={true}
          value={enteredLastName}
          onChange={lastNameChangeHandler}
        >
          Last Name
        </AuthField>
        <AuthField
          required={true}
          inputIsValid={usernameIsValid}
          value={enteredUsername}
          onChange={usernameChangeHandler}
          onBlur={validateUsernameHandler}
        >
          User Name
        </AuthField>
        <AuthField
          title={emailIsValid ? "Email" : "Email Format is Invalid"}
          required={true}
          inputIsValid={emailIsValid}
          value={enteredEmail}
          onChange={emailChangeHandler}
          onBlur={validateEmailHandler}
        >
          Email
        </AuthField>
        <AuthField
          title={
            passwordIsValid
              ? "Password"
              : "Password must be at least 6 characters long"
          }
          required={true}
          inputIsValid={passwordIsValid}
          type="password"
          value={enteredPassword}
          onChange={passwordChangeHandler}
          onBlur={validatePasswordHandler}
        >
          Password
        </AuthField>
        <AuthField
          title={
            confirmedPasswordIsValid
              ? "Confirm Password"
              : "Passwords do not match"
          }
          required={true}
          type="password"
          inputIsValid={confirmedPasswordIsValid}
          value={confirmedPassword}
          onChange={confirmedPasswordChangeHandler}
          onBlur={validateConfirmedPasswordHandler}
        >
          Confirm Password
        </AuthField>
        {response && !response.success ? (
          <div className="flex justify-center text-red-800">
            {response.message}
          </div>
        ) : null}
        <div className={styles.actions}>
          <Button
            className="mt-10"
            type="submit"
            title="alex"
            disabled={!formIsValid}
          >
            Register
          </Button>
        </div>
      </form>
      <GoogleButton />
    </Card>
  );
};

export default RegisterForm;
