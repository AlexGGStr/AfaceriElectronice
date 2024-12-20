import React, { FC } from "react";
import styles from "./LoginPage.module.css";
import LoginForm from "../../components/LoginForm/LoginForm";
import { Link, useSearchParams } from "react-router-dom";
import RegisterForm from "../../components/RegisterForm/RegisterForm";

interface LoginPageProps {}

const LoginPage: FC<LoginPageProps> = () => {
  const [searchParams] = useSearchParams();
  const login = searchParams.get("login");

  return (
    <>
      {login === "true" ? <LoginForm /> : <RegisterForm />}
      <Link
        className="w-1/4 flex justify-center font-inherit cursor-pointer bg-green-700 text-white border-2 border-red-900 px-6 py-2
rounded-md transition-colors hover:text-red-900 hover:bg-white mx-auto"
        to={`/auth?login=${login === "true" ? "false" : "true"}`}
      >
        {login === "true"
          ? "No account? Register here"
          : "Already have an account? Login here"}
      </Link>
    </>
  );
};

export default LoginPage;
