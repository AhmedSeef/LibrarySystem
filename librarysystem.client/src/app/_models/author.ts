export interface Author {
    id: number;
    name: string;
    createdAt: Date;
    editedAt?: Date;
    isDeleted: boolean;
  }