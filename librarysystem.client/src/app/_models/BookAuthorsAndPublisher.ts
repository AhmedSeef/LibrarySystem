import { Author } from "./author";
import { Book } from "./book";
import { Publisher } from "./publisher";

export interface BookAuthorsAndPublisher extends Book {
    authorDto: Author;
    publisherDto: Publisher;
}