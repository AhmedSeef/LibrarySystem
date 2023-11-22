export interface Publisher {
    id: number;
    name: string;
    createdAt: Date;
    editedAt?: Date;
    isDeleted: boolean;
  }