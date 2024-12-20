import React, { FC } from "react";
import styles from "./CategorySelect.module.css";
import { Link, useSearchParams } from "react-router-dom";
import Category from "../../models/Category";
import LinkItem from "../../UI/Link/Link";

interface CategoryProps {
  category: Category;
}

const CategorySelect: FC<CategoryProps> = ({ category }) => {
  const [data] = useSearchParams();
  const catQuery = data.get("category");

  return (
    <LinkItem
      to={
        catQuery === category.id.toString()
          ? "/products?page=1"
          : `/products?category=${category.id}&page=1`
      }
    >
      {category.name}
    </LinkItem>
  );
};

export default CategorySelect;
