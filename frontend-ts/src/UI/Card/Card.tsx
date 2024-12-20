import React, { FC } from "react";

import styles from "./Card.module.css";

interface CardProps {
  children: any;
  className?: string | undefined;
}
const Card: FC<CardProps> = (props) => {
  return (
    <div className={`${styles.card} ${props.className}`}>{props.children}</div>
  );
};

export default Card;
