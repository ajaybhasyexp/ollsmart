import { OrderDetail } from "./OrderDetail";

export class OrderHeader {
    orderHeaderId: number;
    orderNo: string;
    userId:number;
    orderDate:string;
    totalAmount:number
    lineItemCount: number;
    expectedDeliveryDate: string;
    status: number;
    orderDetail: OrderDetail[]
  }