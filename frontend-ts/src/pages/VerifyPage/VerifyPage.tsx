import React, { FC } from "react";
import styles from "./VerifyPage.module.css";
import { useSearchParams } from "react-router-dom";
import useApiCall from "../../util/useApiCall";
import backendUrl from "../../config";

interface VerifyPageProps {}

const VerifyPage: FC<VerifyPageProps> = () => {
  let content = <p>Verifying...</p>;
  const [params] = useSearchParams();
  const verificationToken = params.get("token");

  const { data, isLoading } = useApiCall(
    `${backendUrl}/Auth/verify?token=${verificationToken}`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  if (!isLoading && data) {
    content = <p>{data.message}</p>;
  }

  return <div>{content}</div>;
};

export default VerifyPage;
