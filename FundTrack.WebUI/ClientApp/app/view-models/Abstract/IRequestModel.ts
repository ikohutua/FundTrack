/** for display new requests */
export interface IRequestModel {
    id: number;
    organizationId: number;
    organizationName: string;
    description: string;
    date: Date;
    pathToCoverImage: string;
}