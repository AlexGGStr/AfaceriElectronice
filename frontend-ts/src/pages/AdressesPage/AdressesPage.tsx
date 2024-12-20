import React, { FC } from "react";
import styles from "./AdressesPage.module.css";
import AdressList from "../../components/AdressList/AdressList";

interface AdressesPageProps {}

const AdressesPage: FC<AdressesPageProps> = () => <AdressList />;

export default AdressesPage;
