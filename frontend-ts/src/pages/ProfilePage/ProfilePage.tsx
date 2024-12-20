import React, { FC } from "react";
import styles from "./ProfilePage.module.css";
import ProfilePanel from "../../components/ProfilePanel/ProfilePanel";

interface ProfilePageProps {}

const ProfilePage: FC<ProfilePageProps> = () => (
  <div className={styles.ProfilePage}>
    <ProfilePanel />
  </div>
);

export default ProfilePage;
