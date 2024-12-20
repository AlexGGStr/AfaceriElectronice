import React, { FC, useEffect, useState } from "react";
import styles from "./ProductsPanel.module.css";
import Category from "../../models/Category";
import CategorySelect from "../CategorySelect/CategorySelect";
import Card from "../../UI/Card/Card";
import ProductList from "../ProductList/ProductList";
import useApiCall from "../../util/useApiCall";
import backendUrl from "../../config";
import ListItem from "../../UI/ListItem/ListItem";
import { useSearchParams } from "react-router-dom";
import { Pagination } from "@mui/material";

interface CategoriesPanelProps {}

const ProductsPanel: FC<CategoriesPanelProps> = (props) => {
  const [data] = useSearchParams();
  const catQuery = data.get("category");

  const { data: categoryData, isLoading } = useApiCall<Category[]>(
    `${backendUrl}/Category`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    }
  );

  let content = <p>No categories found</p>;

  if (categoryData && categoryData.data.length > 0) {
    content = (
      <ul className="list-none">
        {categoryData.data.map((category) => (
          <ListItem
            className={`${
              category.id.toString() === catQuery ? "bg-green-500" : ""
            }`}
          >
            <CategorySelect key={category.id} category={category} />
          </ListItem>
        ))}
      </ul>
    );
  }

  if (isLoading) {
    content = <p>Loading...</p>;
  }
  const changehandler = (event: React.ChangeEvent<unknown>, value: number) => {
    console.log(value);
    console.log(event);
  };

  return (
    <Card className="bg-gray-100">
      <div className="flex">
        <Card className="w-1/4 h-fit bg-gray-100 rounded-3xl m-4">
          {content}
        </Card>
        <div className="flex w-full ">
          <ProductList />
        </div>
      </div>
    </Card>
  );
};

export default ProductsPanel;
