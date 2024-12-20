import React, { FC } from 'react';
import styles from './OrdersPage.module.css';

interface OrdersPageProps {}

const OrdersPage: FC<OrdersPageProps> = () => (
  <div className={styles.OrdersPage}>
    OrdersPage Component
  </div>
);

export default OrdersPage;
