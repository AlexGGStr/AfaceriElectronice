import React, { FC } from "react";

import styles from "./Card.module.css";

interface ButtonProps {
  disabled?: boolean;
  type?: any;
  onClick?: any;
  children?: any;
  className?: string | undefined | null;
  title?: string;
}
const Button: FC<ButtonProps> = (props) => {
  return (
    <button
      title={props.title}
      disabled={props.disabled}
      type={props.type}
      onClick={props.onClick}
      className={`w-1/3 mb-2 bt flex justify-center font-inherit cursor-pointer bg-green-700 border-2 border-red-900 px-6 py-2
rounded-md transition-colors hover:text-red-900 hover:bg-white mx-auto ${
        props.disabled ? "opacity-50 cursor-not-allowed" : ""
      } ${props.className}`}
    >
      {props.children}
    </button>
  );
};

export default Button;
