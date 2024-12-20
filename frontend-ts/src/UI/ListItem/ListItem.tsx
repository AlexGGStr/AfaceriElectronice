import React, { FC } from "react";

import styles from "./Card.module.css";
import { Link } from "react-router-dom";

interface ListItemProps {
  children: any;
  className?: string | undefined;
  key?: string | number | null | undefined;
}
const ListItem: FC<ListItemProps> = (props) => (
  <li
    key={props.key}
    className={`hover:bg-green-400 m-1 border border-gray-300 bg-gray-200 p-4 rounded-2xl text-center ${props.className}`}
  >
    {props.children}
  </li>
);

export default ListItem;
