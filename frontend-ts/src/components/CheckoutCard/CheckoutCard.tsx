import React, { FC } from "react";
import styles from "./CheckoutCard.module.css";
import CartItem from "../../models/CartItem";
import { Card } from "react-bootstrap";
import { Link } from "react-router-dom";
import Button from "../../UI/Button/Button";
import LinkItem from "../../UI/Link/Link";

interface CheckoutCardProps {
  item: CartItem;
}

const CheckoutCard: FC<CheckoutCardProps> = ({ item }) => (
  <div className="flex w-3/5 rounded-2xl -mx-4 shadow items-center justify-center">
    <div className="flex flex-row items-center">
      <img
        className=" pl-10 w-36 h-52 object-contain "
        src={item.product.images[0]?.imageName || "/images/default.jpg"}
        alt="Image description"
      />
      <h3 className=" font-bold text-xl pl-14 text-center">
        {item.product.name}
      </h3>
    </div>
  </div>
);

export default CheckoutCard;
