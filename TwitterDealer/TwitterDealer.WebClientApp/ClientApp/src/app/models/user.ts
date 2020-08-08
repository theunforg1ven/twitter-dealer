import { Photo } from './photo';
import { SavedThread } from './savedThread';

export interface User {
    id: number;
    username: string;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    photos?: Photo[];
    savedThreads?: SavedThread[];
}
