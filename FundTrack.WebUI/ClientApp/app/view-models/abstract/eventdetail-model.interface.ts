/** for display new event detail */
export interface IEventDetailModel {
    id: number;
    organizationId: number;
    organizationName: string;
    description: string;
    createDate: Date;
    imageUrl: string[];
}