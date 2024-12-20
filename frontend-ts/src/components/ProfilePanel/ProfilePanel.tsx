import React, { FC } from "react";
import Card from "../../UI/Card/Card";
import ProductCard from "../ProductCard/ProductCard";
import CategorySelect from "../CategorySelect/CategorySelect";
import { Link } from "react-router-dom";
import ListItem from "../../UI/ListItem/ListItem";
import LinkItem from "../../UI/Link/Link";

interface ProfilePanelProps {}

const ProfilePanel: FC<ProfilePanelProps> = () => (
  <div className="flex justify-center items-center">
    <Card className="w-1/2 justify-center">
      <div className="flex justify-center">
        <Card className="w-full bg-gray-100 rounded-2xl m-4 items-center text-center justify-center">
          <ul className="list-none w-full">
            <ListItem>
              <LinkItem to={"/account/adresses"}>Manage Adresses</LinkItem>
            </ListItem>
            <ListItem>
              <LinkItem to={"/account/payments"}>
                Manage Payment Methods
              </LinkItem>
            </ListItem>
          </ul>
        </Card>
      </div>
    </Card>
  </div>
);

export default ProfilePanel;
