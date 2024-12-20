import React, { FC, useEffect, useState } from "react";
import styles from "./Products.module.css";
import { Link, useSearchParams } from "react-router-dom";
import ProductsPanel from "../../components/ProductsPanel/ProductsPanel";
import Category from "../../models/Category";

interface ProductsPageProps {}

const ProductsPage: FC<ProductsPageProps> = () => (
  <>
    <ProductsPanel />
  </>
);

export default ProductsPage;
