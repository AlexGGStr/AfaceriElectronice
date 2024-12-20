import React, { FC } from "react";

import styles from "./Card.module.css";
import { Link } from "react-router-dom";

interface LinkProps {
  className?: string | undefined;
  children: any;
  to: string;
  onClick?: () => void;
}
const LinkItem: FC<LinkProps> = (props) => {
  return (
    <Link
      className={`w-full h-full text-black no-underline px-1 py-1  flex items-center justify-center ${props.className}`}
      to={props.to}
      onClick={props.onClick}
    >
      {props.children}
    </Link>
  );
};

export default LinkItem;
