export interface Book {
    id: number;
    name: string;
    authorId: number;
    publisherId: number;
    createdAt: Date;
    editedAt?: Date;
    isDeleted: boolean;
  }