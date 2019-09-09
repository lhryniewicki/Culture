import React from 'react';
import '../EventPost/EventPost.css';
import Comment from '../Comment/Comment';

class EventPost extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            source: null,
            comments: [],
            showComments: false,
            commentsPage:0
        };

        
        this.showComments = this.showComments.bind(this);
        this.moreComments = this.moreComments.bind(this);

    }
    showComments(event) {
        event.preventDefault();
        if (this.state.comments.length === 0) {
            this.setState({
                comments: [{
                    author: "Author",
                    content: "Jakis tam komentarz",
                    date: "10:35,January 1, 2017"
                },
                {
                    author: "Author2",
                    content: "Jakis tam komentarz2",
                    date: "10:37,January 1, 2017"
                }
                ],
                showComments: !this.state.showComments
            });
        }
        else {
            this.setState({
                showComments: !this.state.showComments
            });
        }
        
    }
    moreComments(event) {
        event.preventDefault();
        let items = [{
            author: "Author",
            content: "Jakis tam komentarz",
            date: "10:35,January 1, 2017"
        },
        {
            author: "Author2",
            content: "Jakis tam komentarz2",
            date: "10:37,January 1, 2017"
            },
           {
                author: "Author3",
                content: "Jakis tam komentarz3",
                date: "10:35,January 1, 2017"
            },
                {
                    author: "Author4",
                    content: "Jakis tam komentarz4",
                    date: "10:37,January 1, 2017"
                }
        ];

        //CALL API SERWER
        this.setState({
            commentsPage: this.state.commentsPage + 1,
            comments:items
        });
    }
    render() {
        return (
            <div className="card mb-4">
                <img className="card-img-top" src="http://placehold.it/750x300" alt="Card image cap" />
                <div className="card-body">
                    <h2 className="card-title">Post Title</h2>
                    <p className="card-text">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis aliquid atque, nulla? Quos cum ex quis soluta, a laboriosam. Dicta expedita corporis animi vero voluptate voluptatibus possimus, veniam magni quis!</p>
                    <a href="#" className="btn btn-primary">Read More &rarr;</a>
                </div>
                <div className="card-footer text-muted">
                    Posted on January 1, 2017 by
            <a href="#">Start Bootstrap</a>
                    <div className="pull-right">
                        <span style={{ marginRight: "50px" }}> Reakcje</span>

                        <a href="#" onClick={this.showComments} className="comment">  Komentarze</a>
                        
                    </div>
                   
                </div>
                {this.state.showComments === true
                    ?
                    <div>
                        
                        {this.state.comments.map((c) =>
                            <Comment content={c.content} author={c.author} creationDate={c.date} />
                        )
                        }
                        <div className="text-center">
                            <a href="#" onClick={this.moreComments}>Załaduj więcej</a>
                        </div>   
                    </div>      

                            :
                            null
                        }
                        
                <div className="card-footer text-muted">
                    <form>
                        <div className="form-group">
                            <input className="form-control input-sm comment" placeholder="Wpisz komentarz..." type="text" />
                        </div>
                    </form>
                </div>
            </div>


        );
    }
}
export default EventPost;
