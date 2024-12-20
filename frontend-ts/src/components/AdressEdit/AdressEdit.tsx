import React, { FC, useEffect, useState } from "react";
import styles from "./AdressEdit.module.css";
import AuthField from "../AuthField/AuthField";
import Button from "../../UI/Button/Button";
import Card from "../../UI/Card/Card";
import useApiCall from "../../util/useApiCall";
import UserAdress from "../../models/UserAdress";
import backendUrl from "../../config";
import { useParams, useSearchParams } from "react-router-dom";

interface AdressEditProps {}

const AdressEdit: FC<AdressEditProps> = () => {
  const params = useParams();

  const { data, isLoading } = useApiCall<UserAdress>(
    `${backendUrl}/UserAdress/${params.adressId}`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    }
  );

  const [adressLine, setAdressLine] = useState<string>("Loading...");
  const [city, setCity] = useState<string>("Loading...");
  const [postalCode, setPostalCode] = useState<string>("Loading...");
  const [phoneNumber, setPhoneNumber] = useState<string>("Loading...");

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

  useEffect(() => {
    if (!isLoading && data) {
      setAdressLine(data.data.adressLine ?? "");
      setCity(data.data.city ?? "");
      setPostalCode(data.data.postalCode ?? "");
      setPhoneNumber(data.data.telephone ?? "");
    }
  }, [data, isLoading]);

  const submitFormHandler = async (event: React.FormEvent) => {
    event.preventDefault();
    await fetch(`${backendUrl}/UserAdress/${params.adressId}`, {
      method: "PUT",
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
    alert("Adress updated successfully!");
    return window.location.replace("/account/adresses");
  };

  const deleteHandler = async () => {
    const check = window.confirm(
      "Are you sure you want to delete this adress?"
    );
    if (check) {
      await fetch(`${backendUrl}/UserAdress/${params.adressId}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });
      return window.location.replace("/account/adresses");
    }
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
        <div>
          <Button className="mt-10" type="submit">
            Edit Adress
          </Button>
        </div>
      </form>
      <Button className="mt-1" onClick={deleteHandler}>
        Delete Adress
      </Button>
    </Card>
  );
};

export default AdressEdit;
