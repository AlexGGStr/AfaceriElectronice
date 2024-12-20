import React, { FC, useEffect } from "react";
import styles from "./ProductCard.module.css";
import Product from "../../models/Product";
import { Card } from "react-bootstrap";
import Button from "../../UI/Button/Button";
import { Link } from "react-router-dom";
import AddCartButton from "../AddCartButton/AddCartButton";

interface ProductCardProps {
  product: Product;
}

const ProductCard: FC<ProductCardProps> = ({ product }) => {
  return (
    <div className="flex w-full rounded-2xl -mx-4 shadow-2xl bg-white ">
      <div className="w-full px-4 mb-4 rounded-2xl">
        <Card>
          <div>
            <img
              className="w-full h-60 overflow-hidden rounded-tl-6 rounded-tr-6 object-contain"
              src={product.images[0]?.imageName || "/images/default.jpg"}
            ></img>
          </div>
          <div>
            <h3 className="text-center p-4">{product.name}</h3>
            <h2 className="text-center">{product.price} RON</h2>
          </div>
          <div className="px-4 py-4 text-center"></div>
          <Link
            className="flex justify-center items-center font-inherit cursor-pointer bg-green-700 text-white border-2 border-red-900 px-6 py-2 rounded-md transition-colors hover:text-red-900 hover:bg-white"
            to={`/products/${product.id}`}
          >
            Details
          </Link>
          <AddCartButton className="w-full" product={product}></AddCartButton>
        </Card>
      </div>
    </div>
  );
};

export default ProductCard;
