import React from "react";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import HomePage from "./pages/HomePage/HomePage";
import MainRoot from "./pages/MainRoot/MainRoot";
import ProductsPage from "./pages/ProductsPage/ProductsPage";
import ProductDetail from "./pages/ProductDetail/ProductDetail";
import LoginPage from "./pages/LoginPage/LoginPage";
import { checkAuthLoader, tokenLoader } from "./util/auth";
import { action as logoutAction } from "./pages/LogoutPage/Logout";
import CartPage from "./pages/CartPage/CartPage";
import ProfilePage from "./pages/ProfilePage/ProfilePage";
import OrdersPage from "./pages/OrdersPage/OrdersPage";
import PaymentsPage from "./pages/PaymentsPage/PaymentsPage";
import AdressesPage from "./pages/AdressesPage/AdressesPage";
import AdressDetail from "./pages/AdressDetail/AdressDetail";
import NewAdressPage from "./pages/NewAdressPage/NewAdressPage";
import PaymentDetail from "./pages/PaymentDetail/PaymentDetail";
import NewPaymentPage from "./pages/NewPaymentPage/NewPaymentPage";
import CheckoutPage from "./pages/CheckoutPage/CheckoutPage";
import CheckoutPaymentPage from "./pages/CheckoutPaymentPage/CheckoutPaymentPage";
import VerifyPage from "./pages/VerifyPage/VerifyPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainRoot />,
    loader: tokenLoader,
    id: "root",
    children: [
      { index: true, element: <HomePage /> },
      {
        path: "/products",
        children: [
          { index: true, element: <ProductsPage /> },
          { path: ":productId", element: <ProductDetail /> },
        ],
      },
      {
        path: "/auth",
        element: <LoginPage />,
      },
      {
        path: "/verifyaccount",
        element: <VerifyPage />,
      },
      {
        path: "/cart",
        element: <CartPage />,
      },
      {
        path: "/checkout",
        loader: checkAuthLoader,
        children: [
          { index: true, element: <CheckoutPage /> },
          { path: "payment", element: <CheckoutPaymentPage /> },
        ],
      },
      {
        path: "/account",
        loader: checkAuthLoader,
        children: [
          { index: true, element: <ProfilePage /> },
          { path: "orders", element: <OrdersPage /> },
          {
            path: "payments",
            children: [
              { index: true, element: <PaymentsPage /> },
              { path: ":paymentId", element: <PaymentDetail /> },
              { path: "new", element: <NewPaymentPage /> },
            ],
          },
          {
            path: "adresses",
            children: [
              { index: true, element: <AdressesPage /> },
              { path: "new", element: <NewAdressPage /> },
              { path: ":adressId", element: <AdressDetail /> },
            ],
          },
        ],
      },
      {
        path: "logout",
        action: logoutAction,
      },
    ],
  },
]);

function App() {
  return <RouterProvider router={router} />;
}

export default App;
