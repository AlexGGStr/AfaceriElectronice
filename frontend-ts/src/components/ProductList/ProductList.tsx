import React, { FC, useState } from "react";
import styles from "./ProductList.module.css";
import useApiCall from "../../util/useApiCall";
import Product from "../../models/Product";
import ProductCard from "../ProductCard/ProductCard";
import { useSearchParams } from "react-router-dom";
import backendUrl from "../../config";
import { Pagination } from "@mui/material";
import { dark } from "@mui/material/styles/createPalette";

interface ProductListProps {
  pageSize?: number;
  homepage?: boolean;
}

const ProductList: FC<ProductListProps> = ({ pageSize = 4, homepage }) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const categoryId = searchParams.get("category") ?? "0";
  const catPage = searchParams.get("page") ?? "0";

  const { data: productData, isLoading } = useApiCall<Product[]>(
    `${backendUrl}/Product?categoryId=${
      categoryId ?? 0
    }&page=${catPage}&pageSize=${pageSize}`
  );

  let content = <p>No Products Found</p>;

  if (productData && productData.data.length > 0) {
    content = (
      <ul className="flex w-full grid grid-cols-4 gap-4">
        {productData.data.map((product) => (
          <li className="mt-3 ml-1 p-1" key={product.id}>
            <ProductCard key={product.id} product={product} />
          </li>
        ))}
      </ul>
    );
  }
  if (isLoading) {
    content = <p>Loading...</p>;
  }

  const changehandler = (event: React.ChangeEvent<unknown>, value: number) => {
    setSearchParams({
      category: categoryId.toString(),
      page: value.toString(),
    });
  };

  return (
    <div className="w-full">
      {content}
      <div>
        {!homepage && (
          <Pagination
            onChange={changehandler}
            className="p-2 m-2"
            count={(productData && parseInt(productData.message)) ?? 0}
            color="primary"
            defaultValue={parseInt(catPage)}
          />
        )}
      </div>
    </div>
  );
};

export default ProductList;
