import React, { FC } from "react";
import styles from "./AuthField.module.css";

interface AuthFieldProps {
  required?: boolean;
  type?: string;
  children?: string;
  inputIsValid?: boolean;
  value?: string;
  onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
  onBlur?: () => void;
  title?: string;
  defaultValue?: string;
}

const AuthField: FC<AuthFieldProps> = ({
  type = "text",
  inputIsValid = true,
  ...props
}) => {
  return (
    <div className={`${styles.control} ${!inputIsValid ? styles.invalid : ""}`}>
      <label htmlFor={props.children?.toLowerCase() || "-"}>
        {props.children}
      </label>
      <input
        defaultValue={props.defaultValue}
        title={props.title}
        required={props.required}
        type={type}
        id={props.children?.toLowerCase() || "-"}
        value={props.value}
        onChange={props.onChange}
        onBlur={props.onBlur}
      />
    </div>
  );
};

export default AuthField;
