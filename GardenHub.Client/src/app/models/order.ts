import { OrderStatus } from "./enums/order-status";

export interface Order {
    title: string;
    location: string;
    price: string;
    isHeartClicked: boolean;
    typeOfWork: string[];
    orderStatus: OrderStatus;
  }