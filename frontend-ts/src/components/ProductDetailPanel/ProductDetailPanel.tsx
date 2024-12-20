import React, { FC, useEffect } from "react";
import styles from "./ProductDetailPanel.module.css";
import { Link, useParams } from "react-router-dom";
import { Card } from "react-bootstrap";
import AddCartButton from "../AddCartButton/AddCartButton";
import useApiCall from "../../util/useApiCall";
import backendUrl from "../../config";
import Product from "../../models/Product";
import Button from "../../UI/Button/Button";
import { Pagination, Slider } from "@mui/material";

interface ProductDetailPanelProps {}

const ProductDetailPanel: FC<ProductDetailPanelProps> = () => {
  const params = useParams();
  const id = params.productId;

  const { data, isLoading } = useApiCall<Product>(
    `${backendUrl}/Product/${id}`
  );
  const product = data?.data;

  const [imageSrc, setImageSrc] = React.useState<string>(
    product?.images[0]?.imageName ?? "/images/default.jpg"
  );
  useEffect(() => {
    setImageSrc(product?.images[0]?.imageName ?? "/images/default.jpg");
  }, [product]);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  const imageHandler = (event: React.ChangeEvent<unknown>, value: number) => {
    setImageSrc(product?.images[value - 1]?.imageName ?? "/images/default.jpg");
  };

  return (
    <>
      <h1 className="pl-40 pb-20 text-5xl font-bold">{product?.name}</h1>
      <div className="flex">
        <div className="w-2/3 flex flex-col justify-center items-center">
          <img
            className="w-full h-96 overflow-hidden rounded-tl-6 rounded-tr-6 object-contain pb-10"
            src={imageSrc || "/images/default.jpg"}
            alt={"image not found"}
          ></img>
          <Pagination
            onChange={imageHandler}
            count={product?.images.length ?? 0}
            color="primary"
            defaultValue={1}
          />
        </div>
        <div>
          <div>
            <h3 className="text-2xl px-40 pb-20 text-center">
              {product?.price ?? "-"} LEI
            </h3>
            <p className="text-center px-40">{product?.description ?? "-"}</p>
          </div>
          <div className="px-4 py-4 text-center pt-10"></div>
          <AddCartButton
            className="w-94.5 h-61.375"
            product={product}
          ></AddCartButton>
        </div>
      </div>
    </>
  );
};

export default ProductDetailPanel;
