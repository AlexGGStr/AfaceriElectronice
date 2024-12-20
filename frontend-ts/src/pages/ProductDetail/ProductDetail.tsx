import React, { FC } from "react";
import styles from "./ProductDetail.module.css";
import { useParams, useSearchParams } from "react-router-dom";
import ProductDetailPanel from "../../components/ProductDetailPanel/ProductDetailPanel";

interface ProductDetailProps {}

const ProductDetail: FC<ProductDetailProps> = () => {
  return <ProductDetailPanel />;
};

export default ProductDetail;
