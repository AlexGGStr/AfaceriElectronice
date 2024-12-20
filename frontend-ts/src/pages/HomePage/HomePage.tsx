import React, { FC, useState, useEffect } from "react";
import styles from "./HomePage.module.css";
import { json, Link, useLoaderData } from "react-router-dom";
import ProductList from "../../components/ProductList/ProductList";

interface HomePageProps {}

const HomePage: FC<HomePageProps> = () => {
  return (
    <div className="bg-gray-100 h-screen">
      <div className="container mx-auto px-4 py-8">
        <h2 className="text-2xl font-bold mb-4">Featured Products</h2>
      </div>
      <div className="pb-20">
        <ProductList homepage={true} />
      </div>

      <div className="bg-gray-800 py-2 text-white text-center bottom-0 left-0 w-full p-4 fixed">
        <h3 className="text-lg font-bold">Contact Information</h3>
        <p>Email: licenta.alex.strz@gmail.com</p>
        <p>Phone: +40787670861</p>
        <p>Address: ASE Street, Bucharest, Romania</p>
        <p className="text-sm">
          &copy; 2023 Alex Ecommerce. All rights reserved.
        </p>
      </div>
    </div>
  );
};

export default HomePage;
