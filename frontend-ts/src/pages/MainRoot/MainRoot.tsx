import React, { FC } from "react";
import styles from "./MainRoot.module.css";
import { Outlet, useLoaderData } from "react-router-dom";
import MainNavigation from "../../components/MainNavigation/MainNavigation";

interface MainRootProps {}

const MainRoot: FC<MainRootProps> = () => {
  return (
    <>
      <MainNavigation />
      <main>
        <Outlet />
      </main>
    </>
  );
};

export default MainRoot;
