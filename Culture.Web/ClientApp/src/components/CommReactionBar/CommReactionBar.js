import React from 'react';
import Comment from '../Comment/Comment';
import Reactions from '../Reactions/Reactions';


const images = {
    like: require('../../assets/reactions/like.svg'),
    love: require('../../assets/reactions/love.svg'),
    wow: require('../../assets/reactions/wow.svg'),
    haha: require('../../assets/reactions/haha.svg'),
    angry: require('../../assets/reactions/angry.svg'),
    sad: require('../../assets/reactions/sad.svg')
};
class CommReactionBar extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            comments: [],
            reactions: {
                angry: 0,
                haha: 0,
                like: 0,
                love: 0,
                sad: 0,
                wow: 0
            },
            reactionsCount: 0,
            commentsCount: 0,
            showReactionModal: false,
            showComments: false,
            commentsPage: 0,
            mouseCoords: 0
        }



        this.showComments = this.showComments.bind(this);
        this.moreComments = this.moreComments.bind(this);
        this.onReactionClick = this.onReactionClick.bind(this);
        this.onReactionSend = this.onReactionSend.bind(this);
        this.closeModal = this.closeModal.bind(this);
        this.sortReactions = this.sortReactions.bind(this);
    }
    closeModal() {
        this.setState({ showReactionModal: false });
    }
    onReactionClick(e) {
        e.preventDefault();
        this.setState({
            mouseCoords: e.clientY
            ,
            showReactionModal: true
        });
    }
    async onReactionSend(e) {
         
        var reactionState = { ...this.state.reactions };
        reactionState[e.target.name] += 1;

        await this.setState({
            reactions: reactionState,
            reactionsCount: this.state.reactionsCount + 1
        });
        this.sortReactions();


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
            comments: items
        });
    }
    sortReactions() {
        var sorted = [];
        for (var reaction in this.state.reactions) {
            sorted.push([reaction, this.state.reactions[reaction]]);
        }
        sorted.sort((a, b) => {
            return b[1] - a[1];
        });
        var result = {};
        sorted.forEach((item) => {
            result[item[0]] = item[1];
        });
        this.setState({ reactions: result });
    }
    displaySortedReactions() {
        var items = [];
        Object.keys(this.state.reactions).forEach((element, index) => {
            if (this.state.reactions[element] > 0)
                items.push(<img draggable={false}
                    onMouseOver={this.onReactionMouseOver}
                    name={element}
                    src={images[element]}
                    width="25px"
                    height="25px"
                    data-toggle="popover"
                    key={index}
                    data-placement="top"
                    title={`Ilość reakcji: ` + this.state.reactions[element]} />)
        });
        return items;
    }
    render() {
        return (
            <div>
                <div className="card-footer text-muted">
                    Umieszczono dnia {this.props.date.day}-{this.props.date.month}-{this.props.date.year} przez
            <a href="#"> Andrzejek12xx </a>
                    {
                        this.displaySortedReactions()
                    }


                    {this.state.reactionsCount}
                    <div className="pull-right">
                        <div className="float-left">
                        <a href="" onClick={this.onReactionClick} className="comment" style={{ marginRight: "50px" }}> Reaguj</a>

                        <a href="" onClick={this.showComments} className="comment">  Komentarze</a>
                        </div>
                    </div>

                    <Reactions images={images}
                        onReactionSend={this.onReactionSend}
                        mouseCoords={this.state.mouseCoords}
                        closeModal={this.closeModal}
                        showModal={this.state.showReactionModal} />

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
export default CommReactionBar;



