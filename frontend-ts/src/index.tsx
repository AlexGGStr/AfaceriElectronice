import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import "./index.css";
import { CartContextProvider } from "./util/cart-context";
import { GoogleLogin, GoogleOAuthProvider } from "@react-oauth/google";
import { clientId } from "./config";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <GoogleOAuthProvider clientId={clientId}>
    <CartContextProvider>
      <React.StrictMode>
        <App />
      </React.StrictMode>
    </CartContextProvider>
  </GoogleOAuthProvider>
);

reportWebVitals();
