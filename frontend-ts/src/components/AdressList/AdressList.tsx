import React, { FC } from "react";
import styles from "./AdressList.module.css";
import Card from "../../UI/Card/Card";
import ListItem from "../../UI/ListItem/ListItem";
import LinkItem from "../../UI/Link/Link";
import useApiCall from "../../util/useApiCall";
import backendUrl from "../../config";
import UserAdress from "../../models/UserAdress";
import Button from "../../UI/Button/Button";
import { useSearchParams } from "react-router-dom";

interface AdressListProps {
  checkout?: boolean;
}

const AdressList: FC<AdressListProps> = ({ checkout = false }) => {
  const { data, isLoading } = useApiCall<UserAdress[]>(
    `${backendUrl}/UserAdress`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    }
  );

  const [params] = useSearchParams();
  const paymentId = params.get("paymentId");
  const adressId = params.get("adressId");

  let content = <p>No adresses found</p>;

  if (!isLoading && data && data.data.length > 0) {
    content = (
      <>
        {data.data.map((adress) => (
          <ListItem
            key={adress.id}
            className={`${
              adress.id.toString() === adressId ? "bg-green-500" : ""
            }`}
          >
            <LinkItem
              to={
                !checkout
                  ? `${adress.id}`
                  : `?adressId=${adress.id}&paymentId=${paymentId}`
              }
              key={adress.id}
            >
              {adress.city}, {adress.adressLine}
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
      <Card className={`${!checkout ? "w-1/2" : "w-full"} justify-center`}>
        <div className="flex justify-center">
          <Card className="w-full bg-gray-100 rounded-2xl m-4 items-center text-center justify-center">
            <ul className="list-none w-full">{content}</ul>
          </Card>
        </div>
        <ListItem className="mx-4 list-none">
          <LinkItem to={"/account/adresses/new"}>Add Adress</LinkItem>
        </ListItem>
      </Card>
    </div>
  );
};

export default AdressList;
