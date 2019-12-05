import React from 'react';
import '../CommReactionBar/CommReactionBar.css';
import { Modal } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import defaultImage from '../../assets/default_avatar.jpg';
import { getUserId, isAdmin } from '../../utils/JwtUtils';
import '../Comment/Comment.css';
import { editComment } from '../../api/CommentApi';


class Comment extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            displayZoom: false,
            redirect: false,
            edit:false
        };

    }

    handleInputChange = (e) =>{
        this.setState({[e.target.name]:  e.target.value});
    }

    closeImageModal = () => {
        this.setState({ displayZoom: false });
    }

    imageClick = () => {
        return <Modal dialogClassName="myModal"  show={this.state.displayZoom} onHide={this.closeImageModal} >
            <img
                    height="700px"
                    src={this.props.image} />
            </Modal>
    }

    zoomImage = () => {
        this.setState({ displayZoom: true });
    }

    editCommentFlag = () => {
        this.setState({edit:true});
    }

    editComment = (e) => {
        e.preventDefault();
        editComment(this.props.commentId, this.state.content);
        this.setState({ edit: false });

    }


    render() {
        return (
            <div className="card-footer coloredPadding">
                {this.state.redirect === true ? this.renderRedirect() : null}
                {this.state.displayZoom ===true ? this.imageClick() : null}
                <div style={{ paddingBottom: "5px" }}>
                    <Link to={`/konto/${this.props.authorId}`} >
                        <img
                            className="mr-3 mb-1"
                            width="50px;"
                            height="50px;"
                            src={this.props.avatarPath !== null ? this.props.avatarPath : defaultImage}
                        />
                            <b >{this.props.author}
                            </b>
                    </Link>
                  
                    <div className="pull-right text-muted px-3 py-3">
                        {this.props.creationDate}
                    </div>
                </div>
                <div className="card-footer commentBox ">
                    {this.state.edit === false ?
                        this.props.content
                        :
                        <form onSubmit={this.editComment}>
                           <input
                                        className="form-control  commentBox"
                                        placeholder="Wpisz komentarz..."
                                        onChange={this.handleInputChange}
                                        required
                                        type="text"
                                        value={this.state.content}
                                        name="content"
                                        autoComplete="off"
                                        height="70px"
                                        width="400px"
                                    />
                        </form>
                        }
                   

                    {getUserId() === this.props.authorId || isAdmin()  ?
                        <div className="pull-right">
                        <i className="fas fa-pencil-alt fa-lg mr-2" onClick={this.editCommentFlag}/>
                        <i className="fas fa-trash-alt fa-lg" onClick={(e) => this.props.deleteComment(e,this.props.commentId)}/>

                        </div>
                        :
                        null
                    }
                   
                    <br style={{ backgroundColor: "#EAEFF5" }} />
                    {this.props.image !== null ?
                        <img
                            onClick={this.zoomImage}
                            className="mt-4"
                            width="300px;"
                            height="300px;"
                            src={this.props.image}
                            alt="Brak zdjęcia!" />
                        :
                        null
                    }
                
                </div>
                
            </div>

        );
    }
}
export default Comment;



