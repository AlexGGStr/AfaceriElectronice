import React, { FC, useEffect, useState } from "react";
import styles from "./NewAdressForm.module.css";
import backendUrl from "../../config";
import Card from "../../UI/Card/Card";
import AuthField from "../AuthField/AuthField";
import Button from "../../UI/Button/Button";

interface NewAdressFormProps {}

const NewAdressForm: FC<NewAdressFormProps> = () => {
  const [adressLine, setAdressLine] = useState<string>("");
  const [city, setCity] = useState<string>("");
  const [postalCode, setPostalCode] = useState<string>("");
  const [phoneNumber, setPhoneNumber] = useState<string>("");

  const changeAdressHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    setAdressLine(event.target.value);
  };

  const changeCityHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCity(event.target.value);
  };

  const changePostalCodeHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPostalCode(event.target.value);
  };

  const changeTelephoneHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPhoneNumber(event.target.value);
  };

  const submitFormHandler = async (event: React.FormEvent) => {
    event.preventDefault();
    await fetch(`${backendUrl}/UserAdress`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      body: JSON.stringify({
        adressLine: adressLine,
        city: city,
        postalCode: postalCode,
        telephone: phoneNumber,
      }),
    });
    return window.location.replace("/account/adresses");
  };

  return (
    <Card className="w-90 max-w-3xl mx-auto my-8 p-8">
      <form onSubmit={submitFormHandler}>
        <AuthField
          required={true}
          value={adressLine}
          onChange={changeAdressHandler}
        >
          Adress Line
        </AuthField>
        <AuthField required={true} value={city} onChange={changeCityHandler}>
          City
        </AuthField>
        <AuthField
          required={true}
          value={postalCode}
          onChange={changePostalCodeHandler}
        >
          Postal Code
        </AuthField>
        <AuthField
          required={true}
          value={phoneNumber}
          onChange={changeTelephoneHandler}
        >
          Phone Number
        </AuthField>
        <div className={styles.actions}>
          <Button className="mt-10" type="submit">
            Add this adress
          </Button>
        </div>
      </form>
    </Card>
  );
};

export default NewAdressForm;
