export class CheckPaymentResponseViewModel {
    rrn: string;
    masked_card: string;
    sender_cell_phone: string;
    response_signature_string: string;
    response_status: string;
    currency: string;
    fee: string;
    reversal_amount: string;
    settlement_amount: string;
    actual_amount: string;
    order_status: string;
    response_description: string;
    order_time: string;
    actual_currency: string;
    order_id: string;
    tran_type: string;
    eci: string;
    settlement_date: string;
    payment_system: string;
    approval_code: string;
    merchant_id: number;
    settlement_currency: string;
    payment_id: string;
    card_bin: string;
    response_code: string;
    card_type: string;
    amount: number;
    sender_email: string;
    signature: string;
    product_id: string;
}

export class FondyCheckPaymentResponseViewModel {
    response: CheckPaymentResponseViewModel;
}