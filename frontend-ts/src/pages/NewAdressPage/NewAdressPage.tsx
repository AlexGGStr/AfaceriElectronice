import React, { FC } from "react";
import styles from "./NewAdressPage.module.css";
import NewAdressForm from "../../components/NewAdressForm/NewAdressForm";

interface NewAdressPageProps {}

const NewAdressPage: FC<NewAdressPageProps> = () => <NewAdressForm />;

export default NewAdressPage;
