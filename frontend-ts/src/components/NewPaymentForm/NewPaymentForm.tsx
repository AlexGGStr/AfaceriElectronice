import React, { FC, useState } from "react";
import styles from "./NewPaymentForm.module.css";
import backendUrl from "../../config";
import Card from "../../UI/Card/Card";
import AuthField from "../AuthField/AuthField";
import Button from "../../UI/Button/Button";
import submitFormHandler from "../../pages/NewPaymentPage/NewPaymentPage";

interface NewPaymentFormProps {
  onSubmit: (
    event: React.FormEvent<HTMLFormElement>,
    accountNo: string,
    paymentType: string
  ) => Promise<void>;
}

const NewPaymentForm: FC<NewPaymentFormProps> = ({ onSubmit }) => {
  const [accountNo, setAccountNo] = useState<string>("");
  const [paymentType, setPaymentType] = useState<string>("");

  const changeAccountNoHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setAccountNo(event.target.value);
  };

  const changePaymentTypeHandler = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPaymentType(event.target.value);
  };

  return (
    <Card className="w-90 max-w-3xl mx-auto my-8 p-8">
      <form onSubmit={(event) => onSubmit(event, accountNo, paymentType)}>
        <AuthField
          required={true}
          value={accountNo}
          onChange={changeAccountNoHandler}
        >
          Account Number
        </AuthField>
        <AuthField
          required={true}
          value={paymentType}
          onChange={changePaymentTypeHandler}
        >
          Payment Type
        </AuthField>
        <div>
          <Button className="mt-10" type="submit">
            Add this payment method
          </Button>
        </div>
      </form>
    </Card>
  );
};

export default NewPaymentForm;
