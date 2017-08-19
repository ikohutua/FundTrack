export class DonateMessages {
    undefinedOrganization: string = 'Будь ласка, оберіть організацію.';
    declinedPayment: string = 'Платіж було скасовано через технічні причини. Будь ласка, повторіть спробу пізніше.'; 
    approvedMessage: string;
    notEnabledDonationFunction: string = "На жаль, ця організація не підключила функцію прийняття пожертв.";
    thank: string = "Дякуємо! :)"

    getApprovedMessage(amount: string, currency: string, orgName: string): string {
        this.approvedMessage = `Ваша пожертва у розмірі ${amount} ${currency} була зарахована на рахунок організації ${orgName}.`;
        return this.approvedMessage;
    }
}