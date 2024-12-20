import React, { FC, useContext } from "react";
import { Form, NavLink, useRouteLoaderData } from "react-router-dom";
import styles from "./MainNavigation.module.css";
import CartContext from "../../util/cart-context";

interface MainNavigationProps {}

const MainNavigation: FC<MainNavigationProps> = () => {
  const token = useRouteLoaderData("root");
  const ctx = useContext(CartContext);

  return (
    <>
      <header className={styles.content}>
        <nav className={styles.navbar}>
          <ul>
            <li>
              <NavLink
                to="/"
                className={({ isActive }) => {
                  return isActive ? styles.active : undefined;
                }}
              >
                Welcome
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/products"
                className={({ isActive }) => {
                  return isActive ? styles.active : undefined;
                }}
              >
                Products
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/account"
                className={({ isActive }) => {
                  return isActive ? styles.active : undefined;
                }}
              >
                Your Profile
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/cart"
                className={({ isActive }) => {
                  return isActive ? styles.active : undefined;
                }}
              >
                Cart {ctx.items.length > 0 && `(${ctx.items.length})`}
              </NavLink>
            </li>
            {!token ? (
              <li>
                <NavLink
                  to="/auth?login=true"
                  className={({ isActive }) => {
                    return isActive ? styles.active : undefined;
                  }}
                >
                  Login
                </NavLink>
              </li>
            ) : (
              <li>
                <Form action="/logout" method="post">
                  <button className={styles.button}>Logout</button>
                </Form>
              </li>
            )}
          </ul>
        </nav>
      </header>
    </>
  );
};

export default MainNavigation;
